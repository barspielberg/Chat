import React, { useContext } from "react";
import { UserContext } from "../../../../Context/UserContext";
import styles from "./User.module.css";

const User = ({ name, isConnected, selected }) => {
  const { setOtherUserName } = useContext(UserContext);

  const classList = [styles.user];
  if (selected) classList.push(styles.selected);
  return (
    <div className={classList.join(" ")} onClick={() => setOtherUserName(name)}>
      <div className={styles.name}>{name}</div>
      <div>{isConnected ? "🟢" : "🔘"}</div>
    </div>
  );
};

export default User;
