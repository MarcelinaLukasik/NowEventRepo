import React from "react";
import { Container, Col, Row } from "react-bootstrap";
import "../styles/footer.css";

export const Footer = () => {
  return (
    <footer className="footer">
      <Container>
        <Row className="footer-text-center">
          <Col size={12} sm={6}>
            <h2 className="footer-title">nowEvent</h2>
          </Col>
          <Col size={12} sm={6}>
            <p className="footer-desc">Copyright 2023. All Rights Reserved</p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
};
