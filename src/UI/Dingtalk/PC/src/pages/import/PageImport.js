import Reflux from "reflux";
import React from "react";
import { Button, message } from "antd";
import dtClientSVC from "../../components/DingtalkClientSVC";

class PageImport extends Reflux.Component {
    async getAuthCode() {
        try {
            let authCode = await dtClientSVC.getDDAuthCodeForMessageAsync();
            if (typeof authCode !== "undefined" && authCode !== null)
                message.success(
                    "成功获取AuthCode：" + JSON.stringify(authCode),
                    10
                );
        } catch (err) {
            message.error(
                "获取AuthCode失败。详细信息：" + JSON.stringify(err),
                10
            );
        }
    }

    render() {
        let t = this;

        return (
            <div>
                <Button
                    onClick={t.getAuthCode.bind(t)}
                    type="primary"
                    size="large"
                    style={{ marginLeft: "10px", padding: "4px 4px" }}
                >
                    导入数据
                </Button>
            </div>
        );
    }
}

export default PageImport;
