import React, { useContext } from "react";
import { UserContext } from "../../../../../Context/UserContext";
import styles from "./Message.module.css";

const Message = ({ message }) => {
  const { user } = useContext(UserContext);
  let sender = message.sender;
  const url = "http://localhost:49683/" + message.imgUrl;

  const time = new Date(message.time);
  const now = new Date();
  const options = { timeStyle: "short" };

  if (
    time.getFullYear() !== now.getFullYear() ||
    time.getMonth() !== now.getMonth() ||
    time.getDay() !== now.getDay()
  )
    options.dateStyle = "short";

  const classes = [styles.row];
  if (sender !== user) {
    classes.push(styles.flip);
  } else {
    sender = "me";
  }

  return (
    <div className={classes.join(" ")}>
      <div className={styles.message}>
        <div className={styles.sender}>{sender}</div>
        {message.imgUrl && <img src={url} alt={message.imgUrl} />}
        <div className={styles.content}>{message.text}</div>
        <div className={styles.time}>
          {time.toLocaleString(undefined, options)}
        </div>
      </div>
    </div>
  );
};
export default React.memo(
  Message,
  (prevProps, nextProps) =>
    prevProps.message.text === nextProps.message.text &&
    prevProps.message.sender === nextProps.message.sender &&
    prevProps.message.time === nextProps.message.time &&
    prevProps.message.imgUrl === nextProps.message.imgUrl
);
