// userService.js

let user = null;

export function setUser(newUser) {
    user = newUser;
}

export function getUser() {
    return user;
}
