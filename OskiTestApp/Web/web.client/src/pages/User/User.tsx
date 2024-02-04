import React, {ReactElement, FC, useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

const User: FC<any> = (): ReactElement => {
    const navigate = useNavigate();

  return <h1>User</h1>
};

export default User;