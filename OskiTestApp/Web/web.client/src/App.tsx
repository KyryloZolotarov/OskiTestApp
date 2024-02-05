import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { routes as appRoutes } from "./routes";
import Layout from "./components/Layout/Layout";
import "./App.css";
import { AuthProvider } from "./Auth/AuthProvider";
import { PrivateRoute } from "./components/routes/PrivateRoute";
function App() {
  const contents = (
    <div>
      <AuthProvider>
        <Router>
          <Layout>
            <Routes>
              {appRoutes.map((route) => (
                <Route
                  key={route.key}
                  path={route.path}
                  element={
                    route.protected ? (
                      <PrivateRoute>
                        <route.component />
                      </PrivateRoute>
                    ) : (
                      <route.component />
                    )
                  }
                />
              ))}
            </Routes>
          </Layout>
        </Router>
      </AuthProvider>
    </div>
  );

  return contents;
}

export default App;
