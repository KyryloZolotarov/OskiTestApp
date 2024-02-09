import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import Cookies from "js-cookie";
// Определение типа для состояния и функций контекста
type AuthContextType = {
  isAuthenticated: boolean;
  setIsAuthenticated: (value: boolean) => void;
  logout: () => Promise<void>; // Изменим тип на Promise<void>
};

// Создание контекста с пустым начальным состоянием
const AuthContext = createContext<AuthContextType | null>(null);

// Определение типов для пропсов провайдера
type AuthProviderProps = {
  children: ReactNode;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const logout = async () => {
    // Здесь можно добавить логику отправки запроса на бэкенд
    try {
      // Ваш код для отправки запроса на бэкенд
      // Пример: await api.logout();
    } catch (error) {
      console.error("Error during logout:", error);
    }

    // Очистим токен и установим флаг аутентификации в false
    Cookies.remove("YourAuthCookieName");
    setIsAuthenticated(false);
  };

  useEffect(() => {
    const checkAuth = async () => {
      const authToken = Cookies.get("YourAuthCookieName");
      setIsAuthenticated(!!authToken);
      setIsLoading(false); // Set loading to false after checking auth token
    };

    checkAuth();
  }, []);

  if (isLoading) {
    return <div>Loading...</div>; // or some loading spinner
  }

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, setIsAuthenticated, logout }}
    >
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
