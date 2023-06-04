using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using ReactiveUI;

namespace FrozenWeb.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string? _conversationId;

    private string? _conversations;

    private bool _isProcessing;
    private string? _question;


    public MainViewModel()
    {
        OnMessageSend = ReactiveCommand.Create(async () =>
        {
            if (string.IsNullOrWhiteSpace(Question)) return;

            await Chat();
        });

        OnSessionReset = ReactiveCommand.Create(() =>
        {
            ConversationId = null;
            Conversations = null;
        });
    }

    public string? Question
    {
        get => _question;
        set => this.RaiseAndSetIfChanged(ref _question, value);
    }

    public string? ConversationId
    {
        get => _conversationId;
        set => this.RaiseAndSetIfChanged(ref _conversationId, value);
    }

    public string? Conversations
    {
        get => _conversations;
        set => this.RaiseAndSetIfChanged(ref _conversations, value);
    }

    public bool IsProcessing
    {
        get => _isProcessing;
        set => this.RaiseAndSetIfChanged(ref _isProcessing, value);
    }

    public ICommand OnMessageSend { get; }
    public ICommand OnSessionReset { get; }

    private async Task Chat()
    {
        IsProcessing = true;
        Conversations += "我: " + Question + "\n";

        var client = new HttpClient();

        try
        {
            var requestUri = "https://d27k6t.laf.run/claude?question=" + HttpUtility.UrlEncode(Question);
            Question = string.Empty;
            if (!string.IsNullOrEmpty(ConversationId)) requestUri += "&conversationId=" + ConversationId;

            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseBody = JObject.Parse(await response.Content.ReadAsStringAsync());

            ConversationId = (string)responseBody["conversationId"]!;
            var returnMsg = (string)responseBody["msg"]!;
            var returnMsgList = new List<string>();
            for (var i = 0; i < returnMsg.Length; i += 30)
                returnMsgList.Add(returnMsg.Substring(i, Math.Min(40, returnMsg.Length - i)));

            Conversations += "Claude: " + returnMsgList.Aggregate((a, b) => a + "\n" + b) + "\n";
            IsProcessing = false;
        }
        catch (Exception e)
        {
            Conversations += "FrozenWeb Error: " + e.Message + "\n";
            IsProcessing = false;
        }
    }
}