// AuthContext.tsx
import React, {
  createContext,
  useState,
  useContext,
  ReactNode,
  useEffect,
} from "react";
import Cookies from "js-cookie";
// Определение типа для состояния и функций контекста
type AuthContextType = {
  isAuthenticated: boolean;
  setIsAuthenticated: (value: boolean) => void;
};

// Создание контекста с пустым начальным состоянием
const AuthContext = createContext<AuthContextType | null>(null);

// Определение типов для пропсов провайдера
type AuthProviderProps = {
  children: ReactNode;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false); // По умолчанию пользователь не залогинен
  useEffect(() => {
    const authToken = Cookies.get('YourAuthCookieName');
    console.log(authToken)
    setIsAuthenticated(!!authToken);
  }, [setIsAuthenticated]);
  return (
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
};

// Hook для удобства использования контекста
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
