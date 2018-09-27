// See https://github.com/Jias/natty-fetch for more details.
import nattyFetch from "natty-fetch";
import settings from "../components/Settings";

const context = nattyFetch.context({
    urlPrefix: settings.webApiUrl,
    mock: false,
    traditional: true,
    timeout: 5000,
    withCredentials: true,
    ignoreSelfConcurrent: true // 开启请求锁 接口在服务端返回响应之前，如果再次被调用，将被忽略
});

context.create("Dingtalk", {
    getConfig: {
        url: "Dingtalk/GetPCConfig"
    }
});

context.create("Test", {
    importData: {
        url: "Test/ImportData",
        method: "POST",
        header: { "Content-Type": "application/json;charset=utf-8" },
        timeout: 60 * 1000
    }
});

export default context.api;
