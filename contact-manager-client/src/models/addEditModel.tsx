import { Contact } from "./contact";

export type ActionModelDialogProps = {  
  onHide: () => void,
  show: boolean,
  isedit: boolean,
  contact: Contact,
  reloadData: () => void
}