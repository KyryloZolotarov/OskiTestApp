import React, {ReactElement, FC, useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

const Home: FC<any> = (): ReactElement => {
    const navigate = useNavigate();

  return <h1>home</h1>
};

export default Home;