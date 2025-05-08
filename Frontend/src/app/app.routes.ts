import { Routes } from '@angular/router';
import { AdminPanelsComponent } from './Components/admin-panels/admin-panels.component';


export const routes: Routes = [
    { path: '', loadComponent: () => import('./Components/home/home.component').then(m => m.HomeComponent) },
    { path: 'account', loadComponent: () => import('./Components/account/account.component').then(m => m.AccountComponent) },
    { path: 'rofus', loadComponent: () => import('./Components/rofus/rofus.component').then(m => m.RofusComponent) },
    { path: 'login', loadComponent: () => import('./Components/login-signup/login-signup.component').then(m => m.LoginSignupComponent) },

    {
        path: 'admin',
        component: AdminPanelsComponent,
        children: 
        [
            { path: 'users', loadComponent: () => import('./Components/admin-panels/admin-users/admin-users.component').then(m => m.AdminUsersComponent) },
            { path: 'games', loadComponent: () => import('./Components/admin-panels/admin-games/admin-games.component').then(m => m.AdminGamesComponent) },
        ]
    },

    
    { path: 'games', loadComponent: () => import('./Components/games-front-page/games-front-page.component').then(m => m.GamesComponent) },
    { path: 'games/dice', loadComponent: () => import('./Components/games/dice-game/dice-game.component').then(m => m.DiceGameComponent) },
    { path: 'games/yatzy', loadComponent: () => import('./Components/games/yatzy-game/yatzy-game.component').then(m => m.YatzyGameComponent) }, 
];
