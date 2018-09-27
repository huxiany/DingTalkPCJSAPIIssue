import settings from "../../components/Settings";
import React from "react";
import { message, Button } from "antd";

class PageExport extends React.Component {
    handlePrintClick() {
        let t = this;

        let webApiUrl = settings.webApiUrl;

        let printAPI = "Test/Print";
        message.info("正在导出数据...");
        window.location = webApiUrl + printAPI;
    }

    render() {
        let t = this;

        return (
            <div>
                <div>
                    <Button
                        type="primary"
                        size="large"
                        style={{ marginLeft: "10px", padding: "4px 4px" }}
                        onClick={t.handlePrintClick.bind(t)}
                    >
                        导出数据
                    </Button>
                </div>
            </div>
        );
    }
}

export default PageExport;
