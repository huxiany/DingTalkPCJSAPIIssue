import Actions from "./actions";
import DB from "../../app/db";
import Reflux from "reflux";

class Store extends Reflux.Store {
    constructor() {
        super();
        this.listenables = Actions;
        this.state = {
            ddConfig: null,
            authCode: null,
            error: false,
            errObj: null,
            loggedIn: false,
            userInfo: null
        };
    }

    onGetConfig(clientUrl, cb) {
        var t = this;
        DB.Dingtalk.getConfig({ clientUrl: clientUrl })
            .then(content => {
                t.setState({ ddConfig: content, error: false, errObj: null });
            })
            .catch(err => {
                t.setState({ error: true, errObj: err });
            })
            .then(() => {
                cb && cb(t.state);
            });
    }
}

export default Store;
