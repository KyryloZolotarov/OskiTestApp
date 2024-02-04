import React, {ReactElement, FC, useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

const Test: FC<any> = (): ReactElement => {
    const navigate = useNavigate();

  return <h1>Test</h1>
};

export default Test;