import React from "react";
import { useLocation } from "react-router-dom";
import { useState, useEffect, setErrors } from "react";
import "../styles/tiles.css";

function RequestMessage() {
  const location = useLocation();
  const requestId = location.state.RequestId;
  const [request, setRequest] = useState([]);

  useEffect(() => {
    fetchRequest();
  }, []);

  async function fetchRequest() {
    const res = await fetch(`/offer/${requestId}/GetSingleRequest`);
    res
      .json()
      .then((res) => setRequest(res))
      .then()
      .catch((err) => setErrors(err));
  }

  return (
    <div className="event">
      <div className="row">
        <div className="Event-col-12">
          <div className="registerCompleted">
            Message from: {request.companyName}
          </div>
          <div className="requestMessage">{request.message}</div>
          <div className="registerMessage">
            Company email: {request.contractorEmail}
          </div>
        </div>
      </div>
    </div>
  );
}

export default RequestMessage;
