export interface User {
    id: number,
    firstName: string,
    lastName: string,
    password: string,
    email: string,
    birthDate: number,
    phoneNumber: number,
    newsLetterIsSubscribed: boolean; 
    balance: number,
    profit: number,
    loss: number,
    role: string;
    excludedUntil?: Date; 
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
        newsLetterIsSubscribed: true,
        balance: 0,
        profit: 0,
        loss: 0,
        role: "",
   }
}  