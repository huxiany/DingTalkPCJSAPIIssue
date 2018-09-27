import userInfo from "./UserInfo";

class DingtalkClientSVC {
    public static isJSAPIAvailable() {
        return !(
            typeof DingTalkPC === "undefined" ||
            DingTalkPC === null ||
            typeof DingTalkPC.ua === "undefined" ||
            DingTalkPC.ua === null ||
            typeof DingTalkPC.ua.isInDingTalk === "undefined" ||
            DingTalkPC.ua.isInDingTalk === null ||
            DingTalkPC.ua.isInDingTalk === false
        );
    }

    // 获取钉钉临时授权码，用于在WebAPI的Get方法中
    public static getDDAuthCodeForMessageAsync() {
        return new Promise((f, r) => {
            if (!this.isJSAPIAvailable()) {
                f(null);
            } // 如果客户端钉钉对象不可用（可能是浏览器)，则直接返回成功
            else {
                const ddConfig = userInfo.getDDConfig();
                this.getTempAuthCodeAsync(ddConfig.corpId, ddConfig.agentId)
                    .then(authCode => {
                        f(authCode);
                    })
                    .catch(err => {
                        r(err);
                    });
            }
        });
    }

    // 将授权码添加到需要被发送到服务端的数据中，临时授权码保存在authCode属性中
    public static addAuthCodeForMessageAsync(postData) {
        return new Promise((f, r) => {
            if (!this.isJSAPIAvailable()) {
                f();
            } // 如果客户端钉钉对象不可用（可能是浏览器)，则直接返回成功
            else {
                const ddConfig = userInfo.getDDConfig();
                this.getTempAuthCodeAsync(ddConfig.corpId, ddConfig.agentId)
                    .then(authCode => {
                        if (
                            !(
                                typeof authCode === "undefined" ||
                                authCode === null
                            )
                        ) {
                            postData.authCode = authCode;
                        }
                        f();
                    })
                    .catch(err => {
                        r(err);
                    });
            }
        });
    }

    // 通过corpId和agentId从DingTalkPC.runtime中拿到临时授权码，用于发送消息
    public static getTempAuthCodeAsync(corpId, agentId) {
        return new Promise((f, r) => {
            DingTalkPC.runtime.permission.requestOperateAuthCode({
                corpId,
                agentId,
                onSuccess: result => {
                    f(result.code);
                },
                onFail: err => {
                    r(err);
                }
            });
        });
    }
}

export default DingtalkClientSVC;
