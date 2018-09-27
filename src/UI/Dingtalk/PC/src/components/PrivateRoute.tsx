import React from "react";
import userInfo from "./UserInfo";

import { Redirect, Route } from "react-router-dom";

export default ({ component: Component, ...rest }) => {
    if (!userInfo.loggedIn()) {
        const loginUrl = "/login";
        return (
            <Redirect
                to={{
                    pathname: loginUrl,
                    state: {
                        originalUrl: window.location.href,
                        nextPathname: rest.location.pathname
                    }
                }}
            />
        );
    }

    return <Route component={Component} {...rest} />;
};
