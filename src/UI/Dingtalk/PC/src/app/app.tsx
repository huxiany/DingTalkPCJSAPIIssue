// tslint:disable-next-line:no-submodule-imports
import "antd/dist/antd.css";

import "babel-polyfill";
import React from "react";
import ReactDOM from "react-dom";
import { RouteComponentProps } from "react-router";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import PrivateRoute from "../components/PrivateRoute";
import "./app.css";

import PageExport from "../pages/export";
import PageImport from "../pages/import";
import PageLogin from "../pages/login";

import { Icon, Layout, Menu } from "antd";
const { Header, Content, Sider } = Layout;
class App extends React.Component<RouteComponentProps<App>, {}> {
    constructor(props) {
        super(props);
    }

    public render(): JSX.Element {
        const t = this;
        const menus = [];

        menus.push(<Menu.Item key="5">导出数据</Menu.Item>);

        menus.push(<Menu.Item key="6">导入数据</Menu.Item>);

        return (
            <Layout>
                <Header className="header">
                    <div className="logo" />
                    <Menu
                        theme="dark"
                        mode="horizontal"
                        style={{ lineHeight: "64px" }}
                    >
                        <Menu.Item key="1">测试</Menu.Item>
                    </Menu>
                </Header>
                <Layout>
                    <Sider width={200} style={{ background: "#fff" }}>
                        <Menu
                            mode="inline"
                            defaultSelectedKeys={["1"]}
                            style={{ height: "100%" }}
                            onClick={t.handleMenuClick.bind(t)}
                        >
                            {menus}
                        </Menu>
                    </Sider>
                    <Layout style={{ padding: "0 24px 24px" }}>
                        <Content
                            style={{
                                background: "#fff",
                                padding: 24,
                                margin: 0,
                                minHeight: 280
                            }}
                        >
                            <Switch>
                                <PrivateRoute
                                    path="/import"
                                    component={PageImport}
                                />
                                <PrivateRoute
                                    path="/export"
                                    component={PageExport}
                                />
                            </Switch>
                        </Content>
                    </Layout>
                </Layout>
            </Layout>
        );
    }

    private handleMenuClick(value) {
        let path = "/";
        switch (value.key) {
            case "5":
                path = "/export";
                break;
            case "6":
                path = "/import";
                break;
        }

        this.props.history.push(path);
    }
}

ReactDOM.render(
    <BrowserRouter>
        <Switch>
            <Route path="/login" component={PageLogin} />
            <PrivateRoute path="/" component={App} />
        </Switch>
    </BrowserRouter>,
    document.getElementById("App")
);
