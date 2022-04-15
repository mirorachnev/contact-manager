import { Contact } from "./contact";

export type ActionModelDialogProps = {  
  onHide: () => void,
  show: boolean,
  reloadData: () => void,
  contact?: Contact
}