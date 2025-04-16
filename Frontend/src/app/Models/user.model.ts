export interface User {
    id: number,
    firstName: string,
    lastName: string,
    password: string,
    email: string,
    birthDate: number,
    phoneNumber: number,
    newsLetterIsSubscribed: false,
    balance: number,
    profit: number,
    loss: number,
    role: string;
    excludedUntil: null;
}

export function resetUser(): User {
    return {
        id: 0,
        firstName: "",
        lastName: "",
        password: "",
        email: "",
        birthDate: 0,
        phoneNumber: 0,
        newsLetterIsSubscribed: false,
        balance: 0,
        profit: 0,
        loss: 0,
        role: "",
        excludedUntil: null
    }
}  