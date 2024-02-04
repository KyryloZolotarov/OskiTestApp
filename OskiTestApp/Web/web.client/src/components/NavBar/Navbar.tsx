import { ReactElement, FC } from "react";
import { useAuth } from "../../Auth/AuthProvider";

const Navbar: FC<any> = (): ReactElement => {
  const { isAuthenticated } = useAuth();

  return (
    <div>
      {!isAuthenticated ? (
        <a href="/">you are not logged in click to login</a>
      ) : (
        <div>
          <h1>navbar</h1>
        </div>
      )}
    </div>
  );
};

export default Navbar;
