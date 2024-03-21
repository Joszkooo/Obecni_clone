// userService.js

let user = null;

export const setUser = (newUser) => {
    user = newUser;
};

export const getUser = () => {
    return user;
};
