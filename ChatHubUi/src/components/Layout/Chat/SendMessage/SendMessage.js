import React, { useState } from "react";
import styles from "./SendMessage.module.css";

const SendMessage = ({ onSendText }) => {
  const [text, setText] = useState("");

  return (
    <React.Fragment>
      <textarea
        className={styles.textarea}
        value={text}
        onChange={(event) => setText(event.target.value)}
      />
      <button
        disabled={!text}
        onClick={() => {
          onSendText(text);
          setText("");
        }}
      >
        Send
      </button>
    </React.Fragment>
  );
};

export default SendMessage;
