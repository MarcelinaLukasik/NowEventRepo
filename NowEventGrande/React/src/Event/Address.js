import React from "react";
import "../styles/event.css";
import { useState, useEffect, useRef } from "react";
import {
  StandaloneSearchBox,
  GoogleMap,
  LoadScript,
  Marker,
  InfoWindow,
} from "@react-google-maps/api";

const placesLibrary = ["places"];

function Address(props) {
  const [key, setKey] = useState();
  const inputRef = useRef();
  const [latitude, setLat] = useState(-3.7281443);
  const [longitude, setLng] = useState(-38.6619214);
  const [mapInstance, setMapInstance] = useState();
  const [activeMarker, setActiveMarker] = useState(false);
  const [address, setAddress] = useState(null);
  const [openingHours, setOpeningHours] = useState();
  const [placeId, setPlaceId] = useState();
  const [placeStatus, setPlaceStatus] = useState();
  const [isValid, setValid] = useState(true);

  const handlePlaceChanged = () => {
    const [place] = inputRef.current.getPlaces();
    if (place) {
      if (place.opening_hours) {
        setOpeningHours(place.opening_hours.weekday_text);
      }
      setPlaceId(place.place_id);
      setLat(place.geometry.location.lat());
      setLng(place.geometry.location.lng());
      console.log(place.geometry.location.lng());
      console.log(String(place.business_status));
      setPlaceStatus(String(place.business_status));
      setAddress(String(place.formatted_address));
    }
  };

  useEffect(() => {
    handleFetch();
  }, []);

  async function handleFetch() {
    const key = await fetchKey();
    setKey(key);
  }

  async function fetchKey() {
    const res = await fetch(`/location/GetMapsKey`);
    const dataJ = await res.text();
    const result = dataJ;
    return result;
  }
  const containerStyle = {
    margin: "auto",
    width: "800px",
    height: "400px",
  };

  const center = {
    lat: latitude,
    lng: longitude,
  };

  const handleMarker = () => {
    setActiveMarker(true);
  };

  async function handleLocationSave() {
    const res = await fetch(`/location/SaveLocation`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "x-access-token": "token-value",
      },
      body: JSON.stringify({
        FullAddress: address,
        Latitude: latitude,
        Longitude: longitude,
        PlaceId: placeId,
        PlaceOpeningHours: String(openingHours),
        PlaceStatus: placeStatus,
        EventId: props.eventId,
      }),
    });

    if (!res.ok) {
      const message = `An error has occured: ${res.status} - ${res.statusText}`;
      setValid(false);
      throw new Error(message);
    } else {
      setValid(true);
      props.setFetchCurrentProgress(true);
    }
  }

  return (
    <div>
      <div className="map">
        {key && (
          <LoadScript googleMapsApiKey={key} libraries={placesLibrary}>
            <StandaloneSearchBox
              onLoad={(ref) => (inputRef.current = ref)}
              onPlacesChanged={handlePlaceChanged}
            >
              <input
                type="text"
                className="form-control"
                placeholder="Enter Location"
              />
            </StandaloneSearchBox>
            <GoogleMap
              onLoad={(map) => setTimeout(() => setMapInstance(map))}
              mapContainerStyle={containerStyle}
              center={center}
              zoom={15}
            >
              {mapInstance && (
                <Marker
                  key="marker_1"
                  position={{ lat: latitude, lng: longitude }}
                  icon={{
                    url: require("../images/icons/party_emoji.png"),
                    fillColor: "#EB00FF",
                    scale: 7,
                  }}
                  onClick={() => handleMarker()}
                >
                  {activeMarker && (
                    <InfoWindow onCloseClick={() => setActiveMarker(false)}>
                      <div className="infoWindow">
                        <div>{address} </div>
                        <div>{openingHours}</div>
                      </div>
                    </InfoWindow>
                  )}
                </Marker>
              )}
              <></>
            </GoogleMap>
          </LoadScript>
        )}
      </div>
      <div className="row">
        <input
          type="button"
          className="saveDate"
          value="Save location"
          onClick={handleLocationSave}
        />
      </div>
      <div>
        {!isValid && <p className="wrongInputMessage">Invalid location!</p>}
      </div>
    </div>
  );
}

export default Address;
