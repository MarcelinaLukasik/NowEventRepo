import "../styles/account.css";
import React from "react";
import { useState } from "react";
import { Eye, EyeSlash } from "react-bootstrap-icons";
import { FormFields } from "../Event/Utils/FormFields";

function Registration() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [confrimPassword, setConfirmPassword] = useState("");
  const [valid, setValid] = useState(true);
  const [isCompleted, setIsCompleted] = useState(false);
  const [passwordType, setPasswordType] = useState(FormFields.Password);

  function handlePost(evt) {
    evt.preventDefault();
    async function fetchData() {
      const res = await fetch(`/account/Register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          FirstName: firstName,
          LastName: lastName,
          Email: email,
          Password: password,
          UserName: userName,
        }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        setValid(false);
        throw new Error(message);
      } else {
        await res.json();
        setIsCompleted(true);
      }
    }
    fetchData();
  }

  const togglePasswordVisibility = (evt) => {
    evt.preventDefault();
    if (passwordType === FormFields.Password) {
      setPasswordType("text");
      return;
    }
    setPasswordType(FormFields.Password);
  };

  return (
    <div className="event">
      {!isCompleted && (
        <div>
          <h2>Create a new account</h2>
          <form className="registerForm" method="post">
            <hr />
            <div
              asp-validation-summary="ModelOnly"
              className="text-danger"
            ></div>
            <div className="form-floating">
              <input
                asp-for="Input.Email"
                className="registerInput"
                autoComplete="username"
                aria-required="true"
                placeholder={FormFields.Email}
                value={email}
                onChange={(event) => {
                  setEmail(event.target.value);
                }}
              />
              <span
                asp-validation-for="Input.Email"
                className="text-danger"
              ></span>
            </div>
            <div>
              <input
                asp-for="Input.FirstName"
                className="registerInput"
                aria-required="true"
                placeholder={FormFields.FirstName}
                value={firstName}
                onChange={(event) => {
                  setFirstName(event.target.value);
                }}
              />
              <span
                asp-validation-for="Input.FirstName"
                className="text-danger"
              ></span>
            </div>
            <div>
              <input
                asp-for="Input.LastName"
                className="registerInput"
                aria-required="true"
                placeholder={FormFields.LastName}
                value={lastName}
                onChange={(event) => {
                  setLastName(event.target.value);
                }}
              />
              <span
                asp-validation-for="Input.LastName"
                className="text-danger"
              ></span>
            </div>
            <div>
              <input
                asp-for="Input.UserName"
                className="registerInput"
                aria-required="true"
                placeholder={FormFields.UserName}
                value={userName}
                onChange={(event) => {
                  setUserName(event.target.value);
                }}
              />
              <span
                asp-validation-for="Input.UserName"
                className="text-danger"
              ></span>
            </div>
            <div className="passwordInput">
              <input
                asp-for="Input.Password"
                className="registerInput"
                autoComplete="new-password"
                aria-required="true"
                placeholder={FormFields.Password}
                value={password}
                onChange={(event) => {
                  setPassword(event.target.value);
                }}
                type={passwordType}
              />
              <button
                className="visibilityButton"
                onClick={togglePasswordVisibility}
              >
                {passwordType === "password" ? (
                  <EyeSlash size={24} color="white" />
                ) : (
                  <Eye size={24} color="white" />
                )}
              </button>
              <span
                asp-validation-for="Input.Password"
                className="text-danger"
              ></span>
            </div>
            <div>
              <input
                asp-for="Input.ConfirmPassword"
                className="registerInput"
                autoComplete="new-password"
                aria-required="true"
                placeholder={FormFields.ConfirmPassword}
                value={confrimPassword}
                onChange={(event) => {
                  setConfirmPassword(event.target.value);
                }}
                type={passwordType}
              />
              <span
                asp-validation-for="Input.ConfirmPassword"
                className="text-danger"
              ></span>
            </div>
            <button
              id="registerSubmit"
              type="submit"
              className="registerButton"
              onClick={handlePost}
            >
              Register
            </button>
            {!valid && (
              <div className="wrongInputMessage">
                Invalid credentials. Try again.
              </div>
            )}
          </form>
        </div>
      )}
      {isCompleted && (
        <div>
          <div className="registerCompleted">Registration completed!</div>
          <div className="registerMessage">Feel free to sing in</div>
        </div>
      )}
    </div>
  );
}

export default Registration;
