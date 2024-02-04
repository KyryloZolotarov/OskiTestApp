import Test from "./pages/Test";
import Home from "./pages/Home";
import Login from "./pages/Login";
import PassedTests from "./pages/PassedTests";
import User from "./pages/User";

// other
import {FC} from "react";


// interface
interface Route {
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<any>
}

export const routes: Array<Route> = [
    {
        key: 'login-route',
        title: 'Login',
        path: '/',
        enabled: true,
        component: Login
    },
    {
        key: 'home-route',
        title: 'Home',
        path: '/index',
        enabled: true,
        component: Home
    },
    {
        key: 'test-route',
        title: 'Test',
        path: '/test',
        enabled: true,
        component: Test
    },
    {
        key: 'passedtests-route',
        title: 'Passed Tests',
        path: '/passedtests',
        enabled: true,
        component: PassedTests
    },
    {
        key: 'user-route',
        title: 'User',
        path: '/user',
        enabled: true,
        component: User
    }
]