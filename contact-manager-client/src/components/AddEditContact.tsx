import React, { Component } from 'react';
import { Button, Form, Modal, } from 'react-bootstrap';
import { ActionModelDialogProps } from '../models/actionModel';
import contactsService from '../services/contactsService';
import { Contact } from '../models/contact';

function AddEditContact(props: ActionModelDialogProps) {    

  const handleOnSubmit = async (ev: any) => {

    ev.preventDefault();
    const contact: Contact = {
      firstName: ev.target.FirstName.value,
      lastName: ev.target.LastName.value,
      email: ev.target.Email.value,
      phoneNumber: ev.target.PhoneNumber.value,
      address: ev.target.Address.value,
      dateOfBirth: ev.target.DateOfBirth.value,
      iban: ev.target.Iban.value,
      id: props.edit ? props.contact?.id! : ''
    };

    if (props.edit) {
      await contactsService.updateContact(contact);
    } else {
      await contactsService.createContact(contact);
    }    

    props.reloadData();
    props.onHide();
  }
  
  return (
    <>
    <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
    >
      <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            {props.edit ? 'Edit Contact': 'Add Contact'}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>

        <Form onSubmit={(e) => handleOnSubmit(e)}>
            <Form.Group>
            <Form.Label>First Name</Form.Label>
              <Form.Control type="text" name="FirstName" required
                defaultValue={props.edit ? props.contact?.firstName : ''}
                placeholder="First Name" />
          </Form.Group>
                   
          <Form.Group>
            <Form.Label>Last Name</Form.Label>
              <Form.Control type="text" name="LastName" required
                defaultValue={props.edit ? props.contact?.lastName : ''}
              placeholder="Last Name" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Email</Form.Label>
              <Form.Control type="text" name="Email" required
                defaultValue={props.edit ? props.contact?.email : ''}
              placeholder="Email" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Phone Number</Form.Label>
              <Form.Control type="text" name="PhoneNumber" required
                defaultValue={props.edit ? props.contact?.phoneNumber : ''}
              placeholder="Phone Number" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Date of Birth</Form.Label>
              <Form.Control type="date" name="DateOfBirth" required
                defaultValue={props.edit ? props.contact?.dateOfBirth?.split('T')[0] : new Date().toISOString().split('T')[0]}
              placeholder="Date of Birth" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Address</Form.Label>
              <Form.Control type="text" name="Address" required
                defaultValue={props.edit ? props.contact?.address : ''}
              placeholder="Address" />
          </Form.Group>
          <Form.Group>
            <Form.Label>IBAN</Form.Label>
              <Form.Control type="text" name="Iban" required
                defaultValue={props.edit ? props.contact?.iban : ''}
              placeholder="Iban" />
          </Form.Group>
          <Form.Group>
            <Button variant="primary" type="submit">
                Save
              </Button>
            </Form.Group>
          </Form>            
      </Modal.Body>

      <Modal.Footer>
        <Button variant="danger" onClick={props.onHide}>Close</Button>
      </Modal.Footer>

      </Modal>
    </>
  );
  
}

export default AddEditContact