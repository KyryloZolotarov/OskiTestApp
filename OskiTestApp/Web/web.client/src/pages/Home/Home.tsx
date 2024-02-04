import React, { ReactElement, FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../Auth/AuthProvider";

const Home: FC<any> = (): ReactElement => {
  const { isAuthenticated } = useAuth();

  return (
    <div>
      {!isAuthenticated ? (
        <a href="/">you are not logged in click to login</a>
      ) : (
        <div>
          <h1>home</h1>
        </div>
      )}
    </div>
  );
};

export default Home;
