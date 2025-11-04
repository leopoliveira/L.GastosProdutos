import RecipeService from '@/common/services/recipe';
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
} from '@chakra-ui/react';

type RecipeDeleteModalProps = {
  phrase: string;
  btnConfirmLabel: string;
  btnCancelLabel: string;
  recipeId: string;
  isOpen: boolean;
  onConfirm: () => void;
  onClose: () => void;
};

const RecipeDeleteModal: React.FC<RecipeDeleteModalProps> = ({
  phrase,
  btnConfirmLabel,
  btnCancelLabel,
  recipeId,
  isOpen,
  onConfirm,
  onClose,
}) => {
  const handleConfirm = () => {
    RecipeService.DeleteRecipe(recipeId);
    onConfirm();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Atenção</ModalHeader>
        <ModalCloseButton />
        <ModalBody>{phrase}</ModalBody>
        <ModalFooter>
          <Button colorScheme="red" mr={3} onClick={handleConfirm}>
            {btnConfirmLabel}
          </Button>
          <Button variant="ghost" onClick={onClose}>
            {btnCancelLabel}
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default RecipeDeleteModal;
