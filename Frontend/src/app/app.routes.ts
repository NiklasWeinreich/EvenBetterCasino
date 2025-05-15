import { Routes } from '@angular/router';
import { AdminPanelsComponent } from './Components/admin-panels/admin-panels.component';
import { RofusComponent } from './Components/rofus/rofus.component';
import { AuthGuard } from './Services/Security/auth.guard.service';


export const routes: Routes = [
    { path: '', loadComponent: () => import('./Components/home/home.component').then(m => m.HomeComponent) },
    { path: 'account', loadComponent: () => import('./Components/account/account.component').then(m => m.AccountComponent) },
    { path: 'rofus', loadComponent: () => import('./Components/rofus/rofus.component').then(m => m.RofusComponent) },
    { path: 'login', loadComponent: () => import('./Components/login-signup/login-signup.component').then(m => m.LoginSignupComponent) },
    { path: 'bank', loadComponent: () => import('./Components/bank/bank.component').then(m => m.BankComponent) },

    {
        path: 'admin',
        component: AdminPanelsComponent,
        canActivate: [AuthGuard],
        children: 
        [
            { path: 'users', loadComponent: () => import('./Components/admin-panels/admin-users/admin-users.component').then(m => m.AdminUsersComponent) },
            { path: 'games', loadComponent: () => import('./Components/admin-panels/admin-games/admin-games.component').then(m => m.AdminGamesComponent) },
            {path:  'newsletter', loadComponent: () => import('./Components/admin-panels/admin-newsletter/admin-newsletter.component').then(m => m.AdminNewsletterComponent) },
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
    },

    { path: 'games', loadComponent: () => import('./Components/games-front-page/games-front-page.component').then(m => m.GamesComponent) },
    { path: 'games/dice', loadComponent: () => import('./Components/games/dice-game/dice-game.component').then(m => m.DiceGameComponent) },
    { path: 'games/yatzy', loadComponent: () => import('./Components/games/yatzy-game/yatzy-game.component').then(m => m.YatzyGameComponent) }, 
    { path: 'games/bombastic', loadComponent: () => import('./Components/games/bombastic/bombastic.component').then(m => m.BombasticComponent) },

    // Sæt ind i en "user / konto", lidt ligesom admin halløjet.
    { path: 'history', loadComponent: () => import('./Components/gamehistory/gamehistory.component').then(m => m.GamehistoryComponent) }

];
