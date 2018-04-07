import * as React from "react";
import { RouteComponentProps } from "react-router";
import { getUserManager, saveToSessionStorage, removeSessionStorage } from "../core/LoginApi";

export class CallBack extends React.Component<RouteComponentProps<{}>, {}>{

    componentDidMount() {
        getUserManager().signinRedirectCallback()
            .then(user => {
                //If not signed in the go back to home page and call sigin.
                if (!user) {
                    removeSessionStorage();
                    return this.props.history.push("/");
                }
                //save token if need
                saveToSessionStorage(user.access_token);

                //goback to home page
                this.props.history.push("/");
            })
            .catch(error => alert(error));
    }

    render() {
        return (<div></div>);
    }
}