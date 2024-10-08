import { IsMongoId, IsNotEmpty } from 'class-validator';

export class DeleteLessonDto {
  @IsNotEmpty()
  @IsMongoId()
  readonly id: string;
}
