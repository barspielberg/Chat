import React, { useContext, useEffect, useState } from "react";
import { UserContext } from "../../../Context/UserContext";
import User from "./User/User";
import styles from "./Users.module.css";

const Users = (props) => {
  const { connection, user, otherUserName } = useContext(UserContext);
  const [usersList, setUsersList] = useState([]);

  useEffect(() => {
    const setUsers = (list) => {
      if (list) setUsersList(list.filter((u) => u.name !== user));
      else setUsersList([]);
    };

    if (connection && user) {
      connection.invoke("GetUsers").then(setUsers).catch(console.log);
      connection.on("UserListUpdated", setUsers);
    }

    return () => {
      if (connection) connection.off("UserListUpdated");
    };
  }, [user, connection]);

  return (
    <div className={styles.users}>
      {usersList.length === 0 ? "you are the first user in the system! please wait for other users to register so you could talk with them" : ""}
      {usersList.map((u, index) => (
        <User key={index} {...u} selected={u.name === otherUserName} />
      ))}
    </div>
  );
};

export default Users;
