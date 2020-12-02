import React, { useEffect, useRef } from "react";
import Message from "./Message/Message";

const Messages = ({ messageList }) => {
  const lastMessage = useRef(null);

  useEffect(() => {
    if (lastMessage.current)
      lastMessage.current.scrollIntoView({ behavior: "smooth" });
  }, [messageList, lastMessage]);

  return (
    <React.Fragment>
      {messageList.map((m, index) => (
        <Message key={index} message={m} />
      ))}
      <div ref={lastMessage} id="last"></div>
    </React.Fragment>
  );
};
export default Messages;
