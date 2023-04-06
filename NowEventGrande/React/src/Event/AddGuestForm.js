import React from "react";
import { useState } from "react";

function AddGuestForm({
  onClick,
  addGuestCount,
  isOpen,
  eventId,
  addChecklistCount,
  setFetchCurrentProgress,
}) {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [isValid, setValid] = useState(true);

  const guestData = [
    {
      Title: "First name",
      Value: firstName,
    },
    {
      Title: "Last name",
      Value: lastName,
    },
    {
      Title: "Email",
      Value: email,
    },
  ];

  function handleSubmit(evt) {
    evt.preventDefault();
    handlePost();
    isOpen = false;
    onClick();
  }

  function setData(evt) {
    switch (evt.target.id) {
      case "First name":
        setFirstName(evt.target.value);
        break;
      case "Last name":
        setLastName(evt.target.value);
        break;
      case "Email":
        setEmail(evt.target.value);
        break;
      default:
        throw new Error("Invalid data");
    }
  }

  function handlePost() {
    async function fetchData() {
      const res = await fetch(`/guest/SaveGuest`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          FirstName: firstName,
          LastName: lastName,
          Email: email,
          EventId: eventId,
        }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        setValid(false);
        throw new Error(message);
      } else {
        setValid(true);
        addGuestCount();
        addChecklistCount();
        setFetchCurrentProgress(true);
      }
    }
    fetchData();
  }

  return (
    <div className="addGuestForm">
      <button className="addButton" onClick={onClick}>
        Add guest
      </button>
      {isOpen && (
        <div className="addGuestContainer">
          <form onSubmit={handleSubmit}>
            {Array.from(guestData).map((dataField, i) => {
              return (
                <div key={i}>
                  <label>{dataField.Title}</label>
                  <input
                    className="addGuest"
                    type="text"
                    value={dataField.Value}
                    required
                    id={dataField.Title}
                    onChange={(e) => setData(e)}
                  />
                </div>
              );
            })}
            <input className="saveGuest" type="submit" value="Save" />
          </form>
        </div>
      )}
      <div>
        {!isValid && (
          <p className="wrongInputMessage">
            Please provide valid name and email!
          </p>
        )}
      </div>
    </div>
  );
}

export default AddGuestForm;
