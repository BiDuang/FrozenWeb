# FrozenWeb
基于 Avalonia WebAssembly 跨平台 UI 的 Claude 聊天解决方案

## Desktop 部分

使用标准的 Avalonia 桌面客户端构建体验，根据目标平台构建后运行即可。

## Browser 部分

FrozenWeb 提供了已部署的快速预览。

提供两种构建模式的站点，启用了 `预先 (AOT) 编译` 的站点需要更久的网络加载时间（站点文件更大），但会 [提供更好的 CPU 密集时性能](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/host-and-deploy/webassembly?view=aspnetcore-7.0#ahead-of-time-aot-compilation)（此项目上体现不明显）。

推荐访问标准站点。

**注意，由于 `ASP .NET WebAssembly` 首次访问需要下载 `dotNet.wasm` 运行时，所以可能需要较久的网络加载时间，请耐心等待。后续访问浏览器会访问缓存，因此响应会更快。**

### 标准站点 （推荐）
[Playground](https://lab-b.114514.bid/)

一般首次加载耗时 15~30 秒。

### 启用了预先 (AOT) 编译的站点
[Playground A](https://lab.114514.bid/)

一般首次加载耗时 40~60 秒。
