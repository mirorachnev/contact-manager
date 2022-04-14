import React, { Component } from 'react';
import { Button, ButtonToolbar, Col, Form, Modal, Row, Table } from 'react-bootstrap';
import { ActionModelDialogProps } from '../models/addEditModel';
import { Contact } from '../models/contact';

export class AddEditContact extends Component<ActionModelDialogProps> {  

  constructor(props: ActionModelDialogProps) {
    super(props);   
  }

  render() {
    return (
      <>
      <Modal
          {...this.props}
          size="lg"
          aria-labelledby="contained-modal-title-vcenter"
          centered
      >
        <Modal.Header closeButton>
            <Modal.Title id="contained-modal-title-vcenter">
              {this.props.isedit ? "Edit " : "Add"} Contact
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>

          <Row>
            <Col sm={6}>
              <Form>
                <Form.Group controlId="Contact">
                  <Form.Label>Contact</Form.Label>
                    <Form.Control type="text" name="FirstName" required
                      placeholder="First Name" value={this.props.contact.firstName} />

                    <Form.Control type="text" name="LastName" required
                      placeholder="Last Name" value={this.props.contact.lastName} />

                    <Form.Control type="text" name="Email" required
                      placeholder="Email" value={this.props.contact.email} />

                    <Form.Control type="text" name="PhoneNumber" required
                      placeholder="Phone Number" value={this.props.contact.phoneNumber} />

                    <Form.Control type="text" name="DateOfBirth" required
                      placeholder="Date of Birth" value={this.props.contact.dateOfBirth} />

                    <Form.Control type="text" name="Address" required
                      placeholder="Address" value={this.props.contact.address} />

                    <Form.Control type="text" name="Iban" required
                      placeholder="Iban" value={this.props.contact.iban} />
                </Form.Group>

                

                

                <Form.Group>
                  <Button variant="primary" type="submit">
                    Add Contact
                  </Button>
                </Form.Group>
              </Form>
            </Col>

            
          </Row>
        </Modal.Body>

          <Modal.Footer>
            <Button variant="danger" onClick={this.props.onHide}>Close</Button>
        </Modal.Footer>

        </Modal>
        </>);
  }
}