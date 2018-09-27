declare interface IOption {
    value: number;
    text: string;
}

declare interface IDDError {
    message: string;
    errorCode: number;
}
type ddErrorFunc = (error: IDDError) => void;

declare interface IRequestOperateAuthCodeParam {
    agentId: string;
    corpId: string;
    onSuccess: (result: { code: string }) => void;
    onFail: ddErrorFunc;
}

declare interface IDingTalkPC {
    runtime: {
        permission: {
            requestOperateAuthCode: (
                param: IRequestOperateAuthCodeParam
            ) => void;
        };
    };
    ua: {
        isInDingTalk: boolean;
    };
}

declare var DingTalkPC: IDingTalkPC;

declare interface IUserInfo {
    userName: string;
    name: string;
    roles: string[];
}
