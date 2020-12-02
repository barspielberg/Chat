import React, { useContext, useState, useEffect } from "react";

import { HubConnectionBuilder } from "@microsoft/signalr";

import { UserContext } from "../../Context/UserContext";
import styles from "./Register.module.css";

const Register = ({ history }) => {
  const [isSignUp, setisSignUp] = useState(true);
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [conPassword, setConPassword] = useState("");
  const [isDisabled, setIsDisabled] = useState(true);
  const [error, setError] = useState("");
  const [connectionMessage, setConnectionMessage] = useState(
    "waiting for connection..."
  );
  const [animation, setAnimation] = useState(false);

  const { setConnection, connection, setUser } = useContext(UserContext);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:49683/ChatHub")
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);
  }, [setConnection]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Connected!");
          setConnectionMessage("");
        })
        .catch((e) => {
          console.log("Connection failed: ", e);
          setConnectionMessage("Connection failed");
        });
    } else setError("No connection");
  }, [connection]);

  useEffect(() => {
    if (!userName || !password) setIsDisabled(true);
    else if (isSignUp && password !== conPassword) setIsDisabled(true);
    else setIsDisabled(false);

    setError("");
  }, [userName, isSignUp, password, conPassword, setIsDisabled, setError]);

  const onSubmitHandler = (event) => {
    event.preventDefault();
    if (connection) {
      const method = isSignUp ? "RegisterUserAsync" : "LoginUserAsync";

      connection
        .invoke(method, { name: userName, password })
        .then((res) => {
          if (res) {
            setUser(userName);
            setAnimation(true);
            setTimeout(()=>history.push('/chat'),500)
          } else {
            setError(
              isSignUp
                ? "username already taken :("
                : "one of your cradentional was incorrect"
            );
          }
        })
        .catch(console.log);
    }
  };

  return (
    <div className={[styles.box, animation ? styles.transition : ""].join(" ")}>
      <div className={styles.upBar}></div>
      {connectionMessage && (
        <div className={styles.error}>{connectionMessage}</div>
      )}
      {error && <div className={styles.error}>{error}</div>}
      <form onSubmit={onSubmitHandler}>
        <div className={styles.filler}></div>
        <div className={styles.row}>
          <label htmlFor="username">Username</label>
          <input
            type="text"
            id="username"
            value={userName}
            onChange={(event) => setUserName(event.target.value)}
          />
        </div>
        <div className={styles.row}>
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </div>
        {isSignUp && (
          <div className={styles.row}>
            <label htmlFor="Conpassword">Confirm password</label>
            <input
              type="password"
              id="Conpassword"
              value={conPassword}
              onChange={(event) => setConPassword(event.target.value)}
            />
          </div>
        )}
        <div className={styles.filler}></div>
        <div className={styles.row}>
          <button type="submit" disabled={isDisabled}>
            {isSignUp ? "Sign up" : "Log in"}
          </button>
          <button type="button" onClick={() => setisSignUp(!isSignUp)}>
            {isSignUp ? "already have an account" : "create new account"}
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
