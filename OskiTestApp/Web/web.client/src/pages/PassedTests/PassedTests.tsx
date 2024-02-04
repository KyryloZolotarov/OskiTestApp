import React, {ReactElement, FC, useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

const PassedTests: FC<any> = (): ReactElement => {
    const navigate = useNavigate();

  return <h1>Passed tests</h1>
};

export default PassedTests;