import React from "react";
import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import "../../styles/tiles.css";
import "../../styles/banner.css";

function PostedEvents() {
  const [requests, setRequests] = useState([]);
  const user = localStorage.getItem("user");
  const navigate = useNavigate();

  useEffect(() => {
    fetchUserId();
  }, []);

  async function GetUserRequests(result) {
    const res = await fetch(`/offer/GetRequestsByUserId`, {
      Authorization: !user
        ? {}
        : { Authorization: "Bearer " + user.accessToken },
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(result),
    });
    res.json().then((res) => setRequests(res));
  }
  async function fetchUserId() {
    const res = await fetch("../account/GetCurrentUserId", {
      headers: { "Content-type": "application/json" },
      Authorization: !user
        ? {}
        : { Authorization: "Bearer " + user.accessToken },
    });
    if (!res.ok) {
      const message = `An error has occured: ${res.status} - ${res.statusText}`;
      throw new Error(message);
    } else {
      await res
        .text()
        .then((result) => {
          GetUserRequests(result);
        })
        .catch(res);
    }
  }

  function goToMessage(evt) {
    evt.preventDefault();
    const requestId = evt.currentTarget.value;
    navigate(`/offer/${requestId}`, { state: { RequestId: requestId } });
  }

  return (
    <div className="event">
      {!user && (
        <div className="notSignedInInfo">
          You need to be signed in to see your messages.
        </div>
      )}
      {user && requests.length !== 0 && (
        <div>
          <h2>Your messages:</h2>
          <div className="row shortTileContainer">
            <div className="Event-col-12">
              {Array.from(requests).map((item, i) => {
                return (
                  <form key={i}>
                    <button
                      className="shortTile"
                      value={item.id}
                      onClick={goToMessage}
                    >
                      <h2 className="shortTileText">
                        Message from: {item.companyName}
                      </h2>
                    </button>
                  </form>
                );
              })}
            </div>
          </div>
        </div>
      )}
      {user && requests.length === 0 && (
        <div className="row shortTileContainer">
          <div className="Event-col-5">
            <div className="createdEventsInfo">
              Look like you don't have any messages yet.
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default PostedEvents;
