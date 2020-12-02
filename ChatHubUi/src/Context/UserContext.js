import React, { useState } from "react";

export const UserContext = React.createContext({
  connection: {},
  setConnection: () => {},
  user: "",
  setUser: () => {},
  otherUserName: "",
  setOtherUserName: () => {},
});

const UserContextProvider = (props) => {
  const [connection, setConnection] = useState(null);
  const [user, setUser] = useState("");
  const [otherUserName, setOtherUserName] = useState("");
  return (
    <UserContext.Provider
      value={{
        connection,
        setConnection,
        user,
        setUser,
        otherUserName,
        setOtherUserName,
      }}
    >
      {props.children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;
