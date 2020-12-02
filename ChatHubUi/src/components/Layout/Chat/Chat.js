import React, { useContext, useEffect, useState } from "react";
import { Redirect } from "react-router";
import { UserContext } from "../../../Context/UserContext";

import styles from "./Chat.module.css";
import Messages from "./Messages/Messages";
import SendImage from "./SendImage/SendImage";
import SendMessage from "./SendMessage/SendMessage";

const Chat = (props) => {
  const { user, connection, otherUserName } = useContext(UserContext);

  const [messagesList, setMessagesList] = useState([]);
  const [isSendText, setIsSendText] = useState(true);

  const onSendTextHandler = (text) => {
    connection
      .invoke("SendMessageToUserAsync", otherUserName, text)
      .then((res) => {
        if (!res) console.log("Not sended");
      })
      .catch(console.log);
  };

  const onSendImgHandler = (file, imgText) => {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("sender", user);
    formData.append("receiver", otherUserName);
    formData.append("text", imgText);
    fetch("http://localhost:49683/ImageUploader", {
      method: "POST",
      body: formData,
    })
      .then((res) => {
        setIsSendText(true);
      })
      .catch(console.log);
  };

  useEffect(() => {
    if (connection) {
      connection.on("reciveMessage", (message) => {
        if (message.sender === otherUserName || message.sender === user)
          setMessagesList([...messagesList, message]);
      });
    }
    return () => {
      if (connection) connection.off("reciveMessage");
    };
  }, [connection, messagesList, setMessagesList, user, otherUserName]);

  useEffect(() => {
    if (connection && user && otherUserName) {
      connection
        .invoke("GetMessagesAsync", user, otherUserName)
        .then((res) => {
          if (res) setMessagesList(res);
        })
        .catch(console.log);
    }
  }, [otherUserName, setMessagesList, connection, user]);

  let content = <Redirect to="/" />;

  if (user && connection) {
    content = (
      <div className={styles.box}>
        <div className={styles.messages}>
          <Messages messageList={messagesList} />
        </div>
        {otherUserName && (
          <div className={styles.sendInput}>
            <div
              className={styles.image}
              onClick={() => setIsSendText(!isSendText)}
            >
              <img
                src={
                  isSendText
                    ? "https://img.icons8.com/cotton/64/000000/image-file-add--v2.png"
                    : "https://img.icons8.com/cotton/64/000000/add-to-chat.png"
                }
                alt={isSendText ? "send image" : "send text"}
              />
            </div>
            {isSendText ? (
              <SendMessage onSendText={onSendTextHandler} />
            ) : (
              <SendImage onSendImg={onSendImgHandler} />
            )}
          </div>
        )}
      </div>
    );
  }
  return content;
};

export default Chat;
