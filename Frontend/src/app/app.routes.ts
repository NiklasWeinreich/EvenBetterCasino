import { Routes } from '@angular/router';
import { AdminPanelsComponent } from './Components/admin-panels/admin-panels.component';
import { RofusComponent } from './Components/rofus/rofus.component';


export const routes: Routes = [
    { path: '', loadComponent: () => import('./Components/home/home.component').then(m => m.HomeComponent) },
    { path: 'games', loadComponent: () => import('./Components/games/games.component').then(m => m.GamesComponent) },
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
    {
        path: 'responsible-gambling',
        component: RofusComponent,
        children: 
        [
            { path: 'overview', loadComponent: () => import('./Components/rofus/oversigt/oversigt.component').then(m => m.OversigtComponent) },
            { path: 'problem-gambling', loadComponent: () => import('./Components/rofus/ludomani/ludomani.component').then(m => m.LudomaniComponent) },
            { path: 'myths-explained', loadComponent: () => import('./Components/rofus/myter/myter.component').then(m => m.MyterComponent) },
        ]
    }
];
