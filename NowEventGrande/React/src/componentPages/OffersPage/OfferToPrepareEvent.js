import React from "react";
import { useEffect, useState } from "react";
import "../../styles/Offers/OffersMainPage.css";
import agent from "../../app/api/agent.js";
import { useParams } from "react-router-dom";

export const OfferToPrepareEvent = () => {
  const [client, setClient] = useState([]);
  const [loading, setLoading] = useState(true);
  const { id } = useParams();
  const [message, setMessage] = useState("");
  const [email, setEmail] = useState("");
  const [companyName, setCompanyName] = useState("");
  const [valid, setValid] = useState(true);
  const [isCompleted, setIsCompleted] = useState(false);

  useEffect(() => {
    agent.Offers.productClientId(parseInt(id))
      .then((client) => setClient(client), console.log(client))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  function handlePost(evt) {
    evt.preventDefault();
    async function fetchData() {
      const res = await fetch(`/offer/PostRequest`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          Message: message,
          ContractorId: 0,
          ContractorEmail: email,
          ClientId: client,
          EventId: id,
          CompanyName: companyName,
        }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        setValid(false);
        throw new Error(message);
      } else {
        const result = await res.json();
        setIsCompleted(true);
      }
    }
    fetchData();
  }

  return (
    <div className="event">
      <div className="row">
        <div className="Event-col-12">
          {!isCompleted && (
            <div>
              <h2>Send message to customer:</h2>
              <form className="registerForm" method="post">
                <hr />
                <div
                  asp-validation-summary="ModelOnly"
                  className="text-danger"
                ></div>
                <div>
                  <textarea
                    className="messageInput"
                    aria-required="true"
                    placeholder="Message..."
                    value={message}
                    onChange={(event) => {
                      setMessage(event.target.value);
                    }}
                  />
                </div>
                <div className="form-floating">
                  <input
                    className="registerInput"
                    autoComplete="username"
                    aria-required="true"
                    placeholder="Email"
                    value={email}
                    onChange={(event) => {
                      setEmail(event.target.value);
                    }}
                  />
                </div>
                <div>
                  <input
                    className="registerInput"
                    aria-required="true"
                    placeholder="Company name..."
                    value={companyName}
                    onChange={(event) => {
                      setCompanyName(event.target.value);
                    }}
                  />
                </div>
                <button
                  id="registerSubmit"
                  type="submit"
                  className="registerButton"
                  onClick={handlePost}
                >
                  Submit
                </button>
              </form>
            </div>
          )}
          {isCompleted && (
            <div>
              <div className="registerCompleted">
                Request to prepare event submitted!
              </div>
              <div className="registerMessage">
                Customer received your message.
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};
