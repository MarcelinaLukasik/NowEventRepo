import "../styles/account.css";
import React from "react";
import { useState } from "react";
import Modal from "react-bootstrap/Modal";
import { useNavigate } from "react-router-dom";
import { Eye, EyeSlash } from "react-bootstrap-icons";

function SignIn() {
  const [password, setPassword] = useState("");
  const [userName, setUserName] = useState("");
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleOpen = () => setShow(true);
  const [loggedUser, setLoggedUser] = useState(
    localStorage.getItem("isLogged")
  );
  const [valid, setValid] = useState(true);
  const navigate = useNavigate();
  const [passwordType, setPasswordType] = useState("password");

  function handleLogin(evt) {
    evt.preventDefault();
    async function fetchData() {
      const res = await fetch(`/account/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ Password: password, UserName: userName }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        setValid(false);
        throw new Error(message);
      } else {
        res.json().then((r) => {
          localStorage.setItem("user", JSON.stringify(r.data));
          localStorage.setItem("isLogged", true);
          setLoggedUser(localStorage.getItem("isLogged"));
          handleClose();
        });
      }
    }
    fetchData();
  }

  async function handleSignOut() {
    const res = await fetch(`/account/Logout`, {
      method: "POST",
    });
    if (res.ok) {
      localStorage.clear();
      setLoggedUser("");
      navigate(`/`);
    }
  }

  const togglePasswordVisibility = (evt) => {
    evt.preventDefault();
    if (passwordType === "password") {
      setPasswordType("text");
      return;
    }
    setPasswordType("password");
  };

  return (
    <div>
      {!loggedUser && <button onClick={handleOpen}>SignIn</button>}
      {loggedUser && <button onClick={handleSignOut}>SignOut</button>}
      <Modal show={show} onHide={handleClose} className="signInModal">
        <Modal.Header>
          <Modal.Title>Sign in</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form method="post">
            <hr />
            <div
              asp-validation-summary="ModelOnly"
              className="text-danger"
            ></div>
            <div>
              <input
                asp-for="Input.UserName"
                className="registerInput"
                autoComplete="username"
                aria-required="true"
                placeholder="UserName"
                value={userName}
                onChange={(evt) => {
                  setUserName(evt.target.value);
                }}
              />
              <label asp-for="Input.Email"></label>
              <span
                asp-validation-for="Input.Email"
                className="text-danger"
              ></span>
            </div>
            <div className="passwordInput">
              <input
                asp-for="Input.Password"
                className="registerInput"
                autoComplete="new-password"
                aria-required="true"
                placeholder="Password"
                value={password}
                onChange={(evt) => {
                  setPassword(evt.target.value);
                }}
                type={passwordType}
              />
              <button
                className="visibilityButton"
                onClick={togglePasswordVisibility}
              >
                {passwordType === "password" ? (
                  <EyeSlash size={24} />
                ) : (
                  <Eye size={24} />
                )}
              </button>
              <label asp-for="Input.Password"></label>
              <span
                asp-validation-for="Input.Password"
                className="text-danger"
              ></span>
            </div>
            <button
              id="registerSubmit"
              type="submit"
              className="registerButton"
              onClick={handleLogin}
            >
              Sign in
            </button>
            {!valid && (
              <div className="wrongInputMessage">Invalid credentials.</div>
            )}
          </form>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default SignIn;
