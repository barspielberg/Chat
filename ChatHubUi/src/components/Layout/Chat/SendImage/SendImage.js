import React, { useEffect, useState } from "react";
import styles from "./SendImage.module.css";

const SendImage = ({ onSendImg }) => {
  const [imgUrl, setImgUrl] = useState(
    "https://img.icons8.com/color/48/000000/upload.png"
  );

  const [file, setFile] = useState(undefined);
  const [imgText, setImgText] = useState("");

  useEffect(() => {
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onloadend = () => {
        setImgUrl(reader.result);
      };
    }
  }, [file]);

  return (
    <React.Fragment>
      <div className={styles.mainContent}>
        <div className={styles.imageUpload}>
          <label htmlFor="file-input">
            <img src={imgUrl} alt="uplaod" />
          </label>
          <input
            id="file-input"
            type="file"
            accept="image/x-png,image/gif,image/jpeg"
            onChange={(event) => setFile(event.target.files[0])}
          />
        </div>
        {file && (
          <input
            type="text"
            value={imgText}
            onChange={(event) => setImgText(event.target.value)}
          />
        )}
      </div>
      <button disabled={!file} onClick={() => onSendImg(file, imgText)}>
        Send
      </button>
    </React.Fragment>
  );
};

export default SendImage;
