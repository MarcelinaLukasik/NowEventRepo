import "../../styles/carousel.css";
import React from "react";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { useLocation } from "react-router-dom";
import { useState } from "react";
import { Themes } from "./Themes";

const CarouselReact = require("react-responsive-carousel").Carousel;

function Carousel() {
  const location = useLocation();
  const eventId = location.state.EventId;
  const [theme, setTheme] = useState("");

  async function saveTheme(event) {
    var selectedTheme = event.target.value;
    setTheme(selectedTheme);
    const res = await fetch(`/events/${eventId}/SetTheme`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "x-access-token": "token-value",
      },
      body: JSON.stringify(selectedTheme),
    });

    if (!res.ok) {
      const message = `An error has occured: ${res.status} - ${res.statusText}`;
      throw new Error(message);
    }
  }

  return (
    <div>
      <h3>Choose your theme:</h3>
      <CarouselReact showArrows={true} autoPlay={true} infiniteLoop={true}>
        {Array.from(Themes).map((theme, i) => {
          return (
            <div key={i}>
              <img
                src={theme.Image}
                className="carouselImage"
                loading="lazy"
                alt="oops"
              />
              <img
                src={theme.Image}
                className="carouselLight"
                loading="lazy"
                alt="oops"
              />
              <input
                className="legend"
                value={theme.Name}
                type="button"
                onClick={saveTheme}
              />
            </div>
          );
        })}
      </CarouselReact>
      <h3>Selected theme: {theme}</h3>
    </div>
  );
}

export default Carousel;
