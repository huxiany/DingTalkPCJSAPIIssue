import React from "react";
import Reflux from "reflux";
import Actions from "./actions";
import Store from "./store";

import { message, Alert, Row, Col } from "antd";

import userInfo from "../../components/UserInfo";

class Login extends Reflux.Component {
    constructor(props) {
        super(props);
        this.state = {
            loggedIn: false,
            isValidUser: false
        };
        this.store = Store;
        this.isDDReady = false;
        this.ddAuthCode = null;
    }

    render() {
        let t = this;
        let s = t.state;
        let msg = null;
        msg = (
            <Alert
                message="登录成功"
                description="正在跳转到首页..."
                type="success"
                showIcon
            />
        );

        return (
            <Row>
                <Col span={6} />
                <Col span={12} className="login">
                    {msg}
                </Col>
                <Col span={6} />
            </Row>
        );
    }

    componentDidMount() {
        this.login();
    }

    redirectToPreviousPage() {
        let t = this;
        let s = t.state;

        let returnToPath = "/import";
        t.props.history.push(returnToPath);
    }

    login() {
        let t = this;
        let s = t.state;

        message.info("正在登录");

        t.getConfig()
            .then(ddConfig => {
                userInfo.setDDConfig(ddConfig);
                return t.configDD(ddConfig);
            })
            .then(() => {
                const ui = {
                    userName: "ABC",
                    name: "ABC",
                    roles: ["Admin"]
                };
                userInfo.setUserInfo(ui);
                t.redirectToPreviousPage();
            })
            .catch(err => {
                message.error(
                    "登录出错，请重试。详细信息：" + JSON.stringify(err)
                );
            });
    }

    getConfig() {
        return new Promise((f, r) => {
            Actions.getConfig(window.location.href, rs => {
                if (rs.error) r(rs.errObj);
                else {
                    f(rs.ddConfig);
                }
            });
        });
    }

    configDD(ddConfig) {
        return new Promise((f, r) => {
            var apiList = {
                jsApiList: [
                    "runtime.info",
                    "runtime.permission.requestOperateAuthCode"
                ]
            };

            var configParam = Object.assign({}, ddConfig, apiList);

            DingTalkPC.config(configParam);
            DingTalkPC.error(err => {
                r(err);
            });
            DingTalkPC.userid = 0;

            DingTalkPC.ready(() => {
                f(ddConfig.corpId);
            });
        });
    }
}

export default Login;
