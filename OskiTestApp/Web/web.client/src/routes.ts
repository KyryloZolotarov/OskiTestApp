import Test from "./pages/Test";
import Home from "./pages/Home";
import Login from "./pages/Login";
import PassedTests from "./pages/PassedTests";

// other
import {FC} from "react";


// interface
interface Route {
    key: string;
    title: string;
    path: string;
    enabled: boolean;
    component: React.ComponentType<any>; // Используйте React.ComponentType
    protected: boolean;
  }

  export const routes: Array<Route> = [
    {
      key: 'login-route',
      title: 'Login',
      path: '/login',
      enabled: true,
      component: Login,
      protected: false,
    },
    {
      key: 'home-route',
      title: 'Home',
      path: '/',
      enabled: true,
      component: Home,
      protected: true,
    },
    {
      key: 'test-route',
      title: 'Test',
      path: '/test',
      enabled: true,
      component: Test,
      protected: true,
    },
    {
      key: 'passedtests-route',
      title: 'Passed Tests',
      path: '/passedtests',
      enabled: true,
      component: PassedTests,
      protected: true,
    },
]