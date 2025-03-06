import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from './todo/login/guard/auth.guard';
import { AppLayoutComponent } from './todo/layout/app.layout.component';

@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path: '',
                component: AppLayoutComponent,
                canActivate: [AuthGuard],
                children: [
                    {
                        path: '',
                        loadChildren: () => import('./todo/todo.module').then(m => m.TodoModule),
                        canActivate: [AuthGuard],
                    },
                    {
                        path: 'home',
                        loadChildren: () => import('./todo/todo.module').then(m => m.TodoModule),
                        canActivate: [AuthGuard],
                    },
                    {
                        path: 'todo',
                        //data: { roles: ["Kullanıcı"] },
                        canActivate: [AuthGuard],
                        children: [
                            {
                                path: '',
                                loadChildren: () => import('./todo/main/main.module').then(m => m.MainModule),
                            },
                            {
                                path: 'form',
                                loadChildren: () => import('./todo/main/main.module').then(m => m.MainModule)
                            },
                            {
                                path: 'form/:id',
                                loadChildren: () => import('./todo/main/main.module').then(m => m.MainModule)
                            }
                        ],
                    },
                    {
                        path: 'tag',
                        //data: { roles: ["Kullanıcı"] },
                        canActivate: [AuthGuard],
                        children: [
                            {
                                path: '',
                                loadChildren: () => import('./todo/tag/tag.module').then(m => m.TagModule),
                            },
                        ],
                    },
                ],
            },
            {
                path: 'login',
                loadChildren: () => import('./todo/login/login.module').then(m => m.LoginModule)
            },
            {
                path: 'register',
                loadChildren: () => import('./todo/register/register.module').then(m => m.RegisterModule)
            },
            {
                path: '**',
                redirectTo: 'home',
            }
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
