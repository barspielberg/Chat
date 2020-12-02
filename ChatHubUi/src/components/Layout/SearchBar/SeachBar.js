import React, { useContext, useState } from "react";
import { UserContext } from "../../../Context/UserContext";

import styles from "./SearchBar.module.css";

const SearchBar = (props) => {
  const {
    connection,
    setOtherUserName,
    otherUserName,
    setUser,
    setConnection,
  } = useContext(UserContext);

  const [username, setUsername] = useState("");
  const [isError, setIsError] = useState(false);

  const onSearchHandler = () => {
    connection
      .invoke("IsUserExistsAsync", username)
      .then((res) => {
        if (res) {
          setOtherUserName(username);
        } else {
          setIsError(true);
        }
        setUsername("");
      })
      .catch(console.log);
  };

  const onLogoutHandler = () => {
    connection.stop().then(() => {
      setConnection("");
      setUser("");
      setOtherUserName("");
    });
  };

  const searchbarClass = styles.searchbar;

  return (
    <div className={searchbarClass}>
      <div>
        <input
          value={username}
          onChange={(event) => setUsername(event.target.value)}
          onFocus={() => setIsError(false)}
        />
        <button disabled={!username} onClick={onSearchHandler}>
          Serch
        </button>
      </div>
      <div>
        {isError && <span className={styles.error}>Not Found!</span>}
        {otherUserName && <b>chat with: {otherUserName}</b>}
      </div>
      <button className={styles.logout} onClick={onLogoutHandler}>
        Logout
      </button>
    </div>
  );
};

export default SearchBar;
