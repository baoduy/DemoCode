import { UserManager, User, UserManagerSettings } from "oidc-client";

const loginConfig: UserManagerSettings = {
    authority: "http://localhost:1000",
    client_id: "test_react",
    redirect_uri: "http://localhost:1112/callback",
    response_type: "id_token token",
    scope: "openid profile testapi",
    post_logout_redirect_uri: "http://localhost:1112",
    automaticSilentRenew: true
};

const mgr = new UserManager(loginConfig);

export function getUserManager() {
    return mgr;
}

const sessionKey = "user_token";

export function saveToSessionStorage(token: string) {
    if (token)
        sessionStorage.setItem(sessionKey, token);
}

export function removeSessionStorage() {
    sessionStorage.removeItem(sessionKey);
}

export function getSessionStorage() {
    return sessionStorage.getItem(sessionKey);
}