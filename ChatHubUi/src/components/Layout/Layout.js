import React from "react";

import Styles from "./Layout.module.css";
import Chat from "./Chat/Chat";
import Users from "./Users/Users";
import SearchBar from "./SearchBar/SeachBar";

const Layout = (props) => {
  return (
    <div className={Styles.box}>
      <SearchBar />
      <Users />
      <Chat />
    </div>
  );
};

export default Layout;
